namespace WebApiForLabs.DataBase.Models
{
	public class Table2
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public virtual List<Table1> Table1 { get; set; }
		//То чему можно присвоить List<>
	}
}
