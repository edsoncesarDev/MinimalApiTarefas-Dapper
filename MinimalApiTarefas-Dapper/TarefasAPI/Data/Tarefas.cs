using System.ComponentModel.DataAnnotations.Schema;

namespace TarefasAPI.Data;

[Table("Tarefas")]
public record Tarefas(int Id, string Atividade, string Status);
