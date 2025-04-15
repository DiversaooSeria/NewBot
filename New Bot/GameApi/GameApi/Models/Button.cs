using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ButtonClickRequest
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [Required(ErrorMessage = "O ID do evento é obrigatório")]
    [StringLength(100, ErrorMessage = "O ID não pode exceder 100 caracteres")]
    public string EventId { get; set; }

    [Required(ErrorMessage = "O nome do botão é obrigatório")]
    [StringLength(50, ErrorMessage = "O nome do botão não pode exceder 50 caracteres")]
    public string ButtonName { get; set; }

    [Required(ErrorMessage = "A data/hora do clique é obrigatória")]
    public DateTime ClickTime { get; set; }

    [BsonElement("AdditionalData")]
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();

    public ButtonClickRequest() { }

    public ButtonClickRequest(string eventId, string buttonName, DateTime clickTime, Dictionary<string, string> metadata)
    {
        EventId = eventId;
        ButtonName = buttonName;
        ClickTime = clickTime;
        Metadata = metadata ?? new Dictionary<string, string>();
    }
}