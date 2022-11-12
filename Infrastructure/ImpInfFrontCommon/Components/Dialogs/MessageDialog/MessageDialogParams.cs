namespace ImpInfFrontCommon.Components.Dialogs.MessageDialog
{
    public class MessageDialogParams
    {
        public string Title { get; set; }
        public string Text { get; set; }

        public MessageDialogParams(string title, string text)
        {
            Title = title;
            Text = text;
        }
    }
}
