using UnityEngine;

public class TankBounds
{
    #region PRIVATE_VARIBLES
    private Vector2[] _points;
    private float _margin;
    private bool _wasCorrected;

    private const float SKIN = 0.001f;         
    private const float MIN_SQR_DISTANCE = 1e-8f;
    #endregion

    #region PUBLIC_VARIBLES
    public Vector2[] points
    {
        get { return _points; }
        private set { _points = value != null ? (Vector2[])value.Clone() : new Vector2[0]; }
    }
    public float margin
    {
        get { return _margin; }
        private set { _margin = value >= 0 ? value : 0; }
    }

    public bool isValid
    {
        get { return _points.Length >= 3; }
    }

    public bool wasCorrected
    {
        get { return _wasCorrected; }
    }
    #endregion

    public TankBounds(Vector2[] _points, float _margin = 0f)
    {
        points = _points;
        margin = _margin;
    }
    public bool Contains(Vector2 point)
    {
        if (!isValid) return true;

        bool inside = false;
        for (int i = 0, j = _points.Length - 1; i < _points.Length; j = i++)
        {
            Vector2 a = _points[i];
            Vector2 b = _points[j];

            if ((a.y > point.y) != (b.y > point.y) &&
                point.x < (b.x - a.x) * (point.y - a.y) / (b.y - a.y) + a.x)
            {
                inside = !inside;
            }
        }
        return inside;
    }
    public bool IsAllowed(Vector2 point)
    {
        if (!isValid) return true;
        if (!Contains(point)) return false;
        if (_margin <= 0f) return true;

        int edge;
        Vector2 closest = ClosestPointOnBoundary(point, out edge);
        return (point - closest).sqrMagnitude >= _margin * _margin - MIN_SQR_DISTANCE;
    }
    public Vector2 Constrain(Vector2 from, Vector2 to)
    {
        _wasCorrected = false;
        if (!isValid || IsAllowed(to)) return to;

        _wasCorrected = true;

        int edge;
        Vector2 edgePoint = ClosestPointOnBoundary(to, out edge);
        Vector2 dir = Contains(to) ? to - edgePoint : edgePoint - to;
        dir = dir.sqrMagnitude > MIN_SQR_DISTANCE ? dir.normalized : InwardNormal(edge);

        Vector2 result = edgePoint + dir * (_margin + SKIN);
        return IsAllowed(result) ? result : from;
    }

    private Vector2 ClosestPointOnBoundary(Vector2 point, out int edgeIndex)
    {
        float bestSqr = float.MaxValue;
        Vector2 best = _points[0];
        edgeIndex = 0;

        for (int i = 0; i < _points.Length; i++)
        {
            Vector2 a = _points[i];
            Vector2 b = _points[(i + 1) % _points.Length];
            Vector2 c = ClosestPointOnSegment(point, a, b);

            float sqr = (point - c).sqrMagnitude;
            if (sqr < bestSqr)
            {
                bestSqr = sqr;
                best = c;
                edgeIndex = i;
            }
        }
        return best;
    }

    private Vector2 InwardNormal(int edgeIndex)
    {
        Vector2 a = _points[edgeIndex];
        Vector2 b = _points[(edgeIndex + 1) % _points.Length];

        Vector2 d = (b - a).normalized;
        Vector2 n = new Vector2(-d.y, d.x);
        Vector2 mid = (a + b) * 0.5f;

        return Contains(mid + n * SKIN) ? n : -n;
    }

    private static Vector2 ClosestPointOnSegment(Vector2 p, Vector2 a, Vector2 b)
    {
        Vector2 ab = b - a;
        float sqr = ab.sqrMagnitude;
        if (sqr < MIN_SQR_DISTANCE) return a;

        float t = Mathf.Clamp01(Vector2.Dot(p - a, ab) / sqr);
        return a + ab * t;
    }
    public Vector2[] GetWorldPoints(PolygonCollider2D source)
    {
        if (source == null) return new Vector2[0];

        Vector2[] local = source.GetPath(0);
        Vector2[] world = new Vector2[local.Length];
        for (int i = 0; i < local.Length; i++)
            world[i] = source.transform.TransformPoint(local[i] + source.offset);
        return world;
    }
}