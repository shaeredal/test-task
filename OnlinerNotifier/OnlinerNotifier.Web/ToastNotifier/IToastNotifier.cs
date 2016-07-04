namespace OnlinerNotifier.ToastNotifier
{
    public interface IToastNotifier
    {
        void Send(string connectionId, string message);
    }
}
