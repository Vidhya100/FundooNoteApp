using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ILabelBL
    {
        public bool CreateLabel(long noteId, long userId, string labelName);
        public IEnumerable<LabelEntity> RetriveLabel(long labelId);
        public bool DeleteLabel(long labelId);
        public bool RenameLabel(long userId, string oldLabelName, string newLabelName);
    }
}
