using NSI.Common.Extensions;
using NSI.Common.Models;
using NSI.Domain.Notifications;
using NSI.EF;
using NSI.Repository.Extensions.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Notifications
{
    public class SmsRepository : ISmsRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public SmsRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add new sms
        /// </summary>
        /// <param name="sms"></param>
        /// <returns></returns>
        public int Add(SmsDomain sms)
        {
            var smsDb = new Sms().FromDomainModel(sms);
            _context.Sms.Add(smsDb);
            _context.SaveChanges();
            return smsDb.SmsId;
        }

        /// <summary>
        /// Update existing sms
        /// </summary>
        /// <param name="sms"></param>
        public void Update(SmsDomain sms)
        {
            var smsDb = _context.Sms.FirstOrDefault(x => x.SmsId == sms.Id);
            smsDb.FromDomainModel(sms);
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete an sms
        /// </summary>
        /// <param name="sms"></param>
        public void Delete(SmsDomain sms)
        {
            var smsDb = _context.Sms.First(x => x.SmsId == sms.Id);
            if (smsDb != null)
            {
                smsDb.FromDomainModel(sms);
                _context.Sms.Remove(smsDb);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Get sms by notification id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ICollection<SmsDomain> GetByNotificationId(int Id)
        {
            var result = _context.Sms
                .Where(x => x.NotificationId == Id)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Get sms by from number
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public SmsDomain GetByFrom(string from)
        {
            return _context.Sms.FirstOrDefault(x => x.From.Equals(from)).ToDomainModel();
        }

        /// <summary>
        /// Get sms by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SmsDomain GetById(int id)
        {
            return _context.Sms.FirstOrDefault(x => x.SmsId == id).ToDomainModel();
        }

        /// <summary>
        /// Get sms by phone number
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public SmsDomain GetByPhoneNumber(string phoneNumber)
        {
            return _context.Sms.FirstOrDefault(x => x.PhoneNumber.Equals(phoneNumber)).ToDomainModel();
        }

        /// <summary>
        /// Search for sms-es with search and filter criteria
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="filterCriteria"></param>
        /// <param name="sortCriteria"></param>
        /// <returns></returns>
        public ICollection<SmsDomain> SearchSms(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            var result = _context.Sms
               .DoFiltering(filterCriteria, FilterSms)
               .DoSorting(sortCriteria, SortSms)
               .DoPaging(paging)
               .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        private Expression<Func<Sms, object>> SortSms(string columnName)
        {
            Expression<Func<Sms, object>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "from":
                    fnc = x => x.From;
                    break;
                case "phonenumber":
                    fnc = x => x.PhoneNumber;
                    break;
                default:
                    break;
            }

            return fnc;
        }

        private Expression<Func<Sms, bool>> FilterSms(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<Sms, bool>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "from":
                    if (!isExactMatch)
                    {
                        fnc = x => (x.From).Contains(filterTerm);
                    }
                    else
                    {
                        fnc = x => x.From == filterTerm;
                    }
                    break;

                case "phonenumber":
                    if (!isExactMatch)
                    {
                        fnc = x => (x.PhoneNumber).Contains(filterTerm);
                    }
                    else
                    {
                        fnc = x => x.PhoneNumber == filterTerm;
                    }
                    break;
            }

            return fnc;
        }
    }
}
