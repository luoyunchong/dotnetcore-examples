using FreeSql;
using FreeSql.DataAnnotations;

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

static int UpdatePassword(string newPassword)
{
    var quotedNewPassword = g.sqlite.Ado.ExecuteScalar("SELECT quote(@newPassword)", new Dictionary<string, string> { { "newPassword", newPassword } }) as string;

    int x = g.sqlite.Ado.ExecuteNonQuery("PRAGMA rekey = " + quotedNewPassword);

    return x;
}

//新密码修改成其他数据后，将无法插入数据
string newPassword = "123qwe";
int x = UpdatePassword(newPassword);
Console.WriteLine("密码修改返回值：" + x);

[Table(Name = "tb_topic_insert")]
class Topic
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    public int Clicks { get; set; }
    public string Title { get; set; }
    public DateTime CreateTime { get; set; }
}


