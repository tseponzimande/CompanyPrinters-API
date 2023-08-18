using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Exercise03ex01.Models
{
    public class Printer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int engenPrintersID { get; set; }
        public string? printerName { get; set; }
        public string? printerMake { get; set; }
        public string? folderToMonitor { get; set; }
        public string? outputType { get; set; }
        public string? fileOutput { get; set; }
        public bool? active { get; set; }
        public DateTime? createTimeStamp { get; set; }
        
    }
}
