using Orchard;
using MainBit.MarkupTags.Models;
using System.Collections.Generic;

namespace MainBit.MarkupTags.Services
{
    public interface IMarkupTagService : IDependency
    {
        MarkupTagRecord Get(int Id);
        List<MarkupTagRecord> Get();
        List<MarkupTagRecord> Get(bool Enable);
        bool Set(int id, string title, string content, string zone, string position, bool enable);
        bool Delete(int Id);
        void Add(string title, string content, string zone, string position, bool enable);
    }
}