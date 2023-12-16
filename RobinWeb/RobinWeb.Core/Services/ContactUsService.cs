using RobinWeb.Core.DTOs.ContactUs;
using RobinWeb.Core.Senders;
using RobinWeb.Core.Services.Interfaces;
using RobinWeb.DataLayer.Context;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Core.Services
{
    public class ContactUsService:IContactUsService
    {
        private RobinWebDBContext _context;
        public ContactUsService(RobinWebDBContext context)
        {
            _context = context;
        }

        public void EditContactUsForm(ContactUsForm contactUs)
        {
            _context.ContactUsFormsTbl.Update(contactUs);
            _context.SaveChanges();
        }

        public ContactUsFormViewModel GetContactUses(int pageId, int take, bool isPosted)
        {
            IQueryable<ContactUsForm> result = _context.ContactUsFormsTbl;
            if (isPosted)
            {
                result = result.Where(r => r.IsPosted);
            }
            else
            {
                result = result.Where(r => !r.IsPosted);

            }

            var skip = (pageId - 1) * take;
            var pageCount = (int)Math.Ceiling(result.Count() / (double)take);

            var model = new ContactUsFormViewModel()
            {
                ContactUses = result.OrderByDescending(c => c.CreateDate).Skip(skip).Take(take).ToList(),
                CurrentPage = pageId,
                PageCount = pageCount,
                StartPage = (pageId - 4 <= 0) ? 1 : pageId - 4,
                EndPage = (pageId + 5 > pageCount) ? pageCount : pageId + 5,
                IsPosted = isPosted
            };
            return model;
        }

        public ContactUsForm GetContactUsFormById(int contactUsFormId)
        {
            return _context.ContactUsFormsTbl.SingleOrDefault(c => c.ContactId == contactUsFormId);
        }

        public void InsertQuestion(ContactUsForm contactUsForm)
        {
            _context.ContactUsFormsTbl.Add(contactUsForm);
            _context.SaveChanges();
        }

        public void SendAnswerForContactUs(ContactUsForm contactUs, string answer)
        {
            if (string.IsNullOrEmpty(answer))
            {
                return;
            }
            try
            {
                var emailBody = $"<h3 style='text-align:center'>{contactUs.FullName} عزیز درخواست پشتیبانی شما پاسخ داده شد. </h3><h4>پاسخ پیام پشتیبانی :</h3>{answer}";
                SendEmail.Send(contactUs.Email, "پاسخ پیام پشتیبانی", emailBody.BuildView());
            }
            catch (Exception e)
            {
                return;
            }
            contactUs.IsPosted = true;
            contactUs.AgentAnswer = answer;
            contactUs.AnswerDate = DateTime.Now;
            EditContactUsForm(contactUs);
        }
    }
}
