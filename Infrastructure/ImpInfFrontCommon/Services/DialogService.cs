using ImpInfFrontCommon.Components.Dialogs.Base;

namespace ImpInfFrontCommon.Services
{
    public class DialogService
    {
        public List<object> Dialogs;

        public void RegisterDialog<TDialog>(TDialog dialog)
        {
            var res = Dialogs.Where(d => d.GetType() == typeof(TDialog)).SingleOrDefault();
            if (res != null) Dialogs.Remove(res);
            Dialogs.Add(dialog);
        }

        public DialogService()
        {
            Dialogs = new List<object>();
        }

        public Task<TResult> Show<TDialog, TParams, TResult>(TParams parameters, bool dontCloseOnAction = false) where TDialog : BaseDialog<TParams, TResult>
        {
            var dialog = Dialogs.Where(d => d.GetType() == typeof(TDialog)).SingleOrDefault();
            if (dialog != null)
            {
                return ((BaseDialog<TParams, TResult>)dialog).Show(parameters, dontCloseOnAction);
            }
            else
            {
                throw new Exception($"Dialog ${typeof(TDialog)} not found");
            }
        }

        public void Close<TDialog, TParams, TResult>() where TDialog : BaseDialog<TParams, TResult>
        {
            (Dialogs.SingleOrDefault(d => d.GetType() == typeof(TDialog)) as TDialog).Hide();
        }

    }
}
