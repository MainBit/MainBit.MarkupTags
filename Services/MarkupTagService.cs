using System.Collections.Generic;
using System.Linq;
using MainBit.MarkupTags.Models;
using Orchard.Data;
using Orchard.Caching;

namespace MainBit.MarkupTags.Services
{
    public class MarkupTagService : IMarkupTagService
    {
        private readonly IRepository<MarkupTagRecord> _repository;
        private readonly ISignals _signals;

        public MarkupTagService(
            IRepository<MarkupTagRecord> repository,
            ISignals signals) 
        {
            _repository = repository;
            _signals = signals;
        }

        public MarkupTagRecord Get(int Id)
        {
            return _repository.Table.Where(x => x.Id == Id).FirstOrDefault();
        }

        public List<MarkupTagRecord> Get()
        {
            return _repository.Table.ToList();
        }

        public List<MarkupTagRecord> Get(bool Enable)
        {
            return _repository.Table.Where(p => p.Enable == Enable).ToList();
        }

        public bool Set(int id, string title, string content, string zone, string position, bool enable)
        {
            var record = Get(id);
            if (record != null)
            {
                record.Title = title;
                record.Content = content;
                record.Zone = zone;
                record.Position = position;
                record.Enable = enable;
                _signals.Trigger("MainBit.MarkupTags.MarkupTagRecordChanged");
                return true;
            }
            return false;
        }

        public bool Delete(int Id)
        {
            var record = Get(Id);
            if (record != null)
            {
                _repository.Delete(record);
                _signals.Trigger("MainBit.MarkupTags.MarkupTagRecordChanged");
                return true;
            }
            return false;
        }

        public void Add(string title, string content, string zone, string position, bool enable)
        {
            _repository.Create(new MarkupTagRecord 
            { 
                Title = title, 
                Content = content,
                Zone = zone,
                Position = position,
                Enable = enable
            });
            _signals.Trigger("MainBit.MarkupTags.MarkupTagRecordChanged");
        }
    }
}