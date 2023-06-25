public interface IObserver
{
    public void OnNotify();
    public void OnNotify(ZoomAction type, int zoom, float adsSpeed);
}
