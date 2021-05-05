using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmailClient.Bll.DTO;
using EmailClient.Data.Entities;
using EmailClient.Models;
using LiteDB;
using MimeKit;

namespace EmailClient.Bll
{
   public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<MailBoxProperties, MailBoxPropertiesDto>().ReverseMap();
            CreateMap<MimeMessage, EmailMessageDto>()
                .ForMember(m => m.Id, e => e.MapFrom(m => m.MessageId))
                .ForMember(m => m.To, e => e.MapFrom(m => m.To.Mailboxes.Select(s=>s.Address)))
                .ForMember(m => m.From, e => e.MapFrom(m => m.From.Mailboxes.First().Address))
                .ForMember(m => m.TextBody, e => e.MapFrom(m => m.TextBody))
                .ForMember(m => m.Subject, e => e.MapFrom(m => m.Subject))
                .ForMember(m => m.AttachmentsNames,
                    e => e.MapFrom(m => m.Attachments.Select(a => a.ContentDisposition.FileName)));


            CreateMap<EmailMessageModel, EmailMessageDto>().ReverseMap();



            CreateMap<EmailAccount, EmailAccountDto>()
                .ForMember(e=>e.Id, c=>c.MapFrom(e=>e.Id))
                .ForMember(e=>e.MailBoxProperties, c=>c.MapFrom(e=>e.MailBoxProperties))
                .ForMember(e=>e.Emails, c=>c.MapFrom(e=>e.Emails))
                .ReverseMap();
        
        }
    }
}
