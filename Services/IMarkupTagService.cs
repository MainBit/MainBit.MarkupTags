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
        bool Set(int Id, string Title, string Content, string Position, bool Enable);
        bool Delete(int Id);
        void Add(string Title, string Content, string Position, bool Enable);
    }
}