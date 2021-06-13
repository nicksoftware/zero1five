using System.ComponentModel.DataAnnotations;
using Zero1Five.Gigs;

namespace Zero1Five.Common
{
    public class SaveFileDto
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}