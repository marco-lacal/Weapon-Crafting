public interface ZoomObserver : WSMObserver
{
    public void OnNotify_ZoomIn(int zoom, float adsSpeed);

    public void OnNotify_ZoomOut(float adsSpeed);
}
