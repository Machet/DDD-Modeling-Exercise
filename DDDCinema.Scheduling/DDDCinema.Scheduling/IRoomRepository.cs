using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDCinema.Scheduling
{
    public interface IRoomRepository
    {
        Room Get(int roomId);
    }
}
