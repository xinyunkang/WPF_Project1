using static FriendOrganizer.UI.View.Services.MessageDialogService;

namespace FriendOrganizer.UI.View.Services
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOkCancelDialog(string text, string title);
    }
}