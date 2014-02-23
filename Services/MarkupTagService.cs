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

        public bool Set(int Id, string Title, string Content, string Position, bool Enable)
        {
            var record = Get(Id);
            if (record != null)
            {
                record.Title = Title;
                record.Content = Content;
                record.Position = Position;
                record.Enable = Enable;
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

        public void Add(string Title, string Content, string Position, bool Enable)
        {
            _repository.Create(new MarkupTagRecord 
            { 
                Title = Title, 
                Content = Content,
                Position = Position,
                Enable = Enable
            });
            _signals.Trigger("MainBit.MarkupTags.MarkupTagRecordChanged");
        }
    }
}