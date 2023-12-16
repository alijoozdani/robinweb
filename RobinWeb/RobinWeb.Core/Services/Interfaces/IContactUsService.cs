using RobinWeb.Core.DTOs.ContactUs;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Core.Services.Interfaces
{
    public interface IContactUsService
    {
        #region ContactUsForm
        ContactUsFormViewModel GetContactUses(int pageId, int take, bool isPosted);
        void SendAnswerForContactUs(ContactUsForm contactUs, string answer);
        void EditContactUsForm(ContactUsForm contactUs);
        void InsertQuestion(ContactUsForm contactUsForm);
        ContactUsForm GetContactUsFormById(int contactUsFormId);
        #endregion
    }
}
