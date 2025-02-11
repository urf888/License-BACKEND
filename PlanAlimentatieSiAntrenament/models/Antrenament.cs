namespace models;
public class Antrenament
{
    public int Id { get; set; }
    public string? TipExercitiu { get; set; }
    public int Durata { get; set; } // în minute
    public int CaloriiConsumate { get; set; }
    public DateTime Data { get; set; }
}