using OvOv.Core.Domain;

namespace OvOv.Core.Models
{
    public class CreateUserDto
    {
        public string Name { get; set; }

        public ExtraJson ExtraJson { get; set; }
    }

}
