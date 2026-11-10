namespace MonProjet.Shared.Models.Treasury;

public record TransactionDto(
    Guid Id,
    DateTime Date,
    string Label,
    decimal Amount,
    bool IsIncome // true = Crédit, false = Débit
);