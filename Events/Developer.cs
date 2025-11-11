namespace Events;

/// <summary>
/// Längerandauernde Arbeit
/// </summary>
internal class Developer
{
	public event Action Start; //Hier sind beliebige Delegates möglich

	public event Action End;

	public event Action<int> Progress;

	public void DoWork()
	{
		Start?.Invoke();

		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(200);
			Progress?.Invoke(i);
		}

		End?.Invoke();
	}
}
