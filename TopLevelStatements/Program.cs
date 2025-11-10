Console.WriteLine(args);

A1 a = new A1();
List<B1> list = [];
a.Liste = list;

public class A
{
	public virtual IEnumerable<B> Liste { get; set; }
}

public class B;

public class A1 : A
{
	private List<B1> privList;

	public override IEnumerable<B> Liste { get => privList; set => privList = value; }
}

public class B1 : B;