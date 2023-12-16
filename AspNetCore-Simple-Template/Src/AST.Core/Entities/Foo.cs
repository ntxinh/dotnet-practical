using AST.Core.Interfaces;

namespace AST.Core.Entities
{
    public class Foo : BaseEntityAudit, IAggregateRoot
    {
        public string Bar { get; set; }
        public string FooBar { get; set; }
    }
}
