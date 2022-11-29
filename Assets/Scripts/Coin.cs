public class Coin : MapObject
{
    private float _rotationSpeed = 0.5f;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(0, _rotationSpeed, 0);
    }
}
