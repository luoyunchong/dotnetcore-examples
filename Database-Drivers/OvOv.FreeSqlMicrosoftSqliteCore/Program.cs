using FreeSql;
using FreeSql.DataAnnotations;
using System.Security.Principal;
using System.Xml.Linq;

Test();

static void Test()
{
    IInsert<Topic> insert = g.sqlite.Insert<Topic>();
    var items = new List<Topic>();
    for (var a = 0; a < 10; a++) items.Add(new Topic { Id = a + 1, Title = $"newTitle{a}", Clicks = a * 100 });

    var affrows = insert.AppendData(items).ExecuteAffrows();
    Console.WriteLine("affrows：" + affrows);
    var list = g.sqlite.Select<Topic>().ToList();
    Console.WriteLine("count：" + list.Count);
}



[Table(Name = "tb_topic_insert")]
class Topic
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    public int Clicks { get; set; }
    public string Title { get; set; }
    public DateTime CreateTime { get; set; }
}
