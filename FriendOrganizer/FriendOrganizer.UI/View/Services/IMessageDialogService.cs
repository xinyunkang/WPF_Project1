namespace FriendOrganizer.UI.View.Services
{
    public interface IMessageDialogService
    {
        MessageDialogService.MessageDialogResult ShowOkCancelDailog(string text, string title);
    }
}