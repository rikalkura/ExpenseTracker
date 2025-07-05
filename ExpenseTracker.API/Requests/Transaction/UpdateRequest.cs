namespace ExpenseTracker.API.Requests.Transaction;

public class UpdateRequest
{
    public decimal Amount { get; set; }
    public DateOnly Date { get; set; }
    public string Comment { get; set; }
}
