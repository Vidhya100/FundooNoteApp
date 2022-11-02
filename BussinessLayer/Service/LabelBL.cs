using BussinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL ilabelRL;

        public LabelBL(ILabelRL ilabelRL)
        {
            this.ilabelRL = ilabelRL;
        }

        public bool CreateLabel(long noteId, long userId, string labelName)
        {
            try 
            {
                return this.ilabelRL.CreateLabel(noteId, userId, labelName);
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IEnumerable<LabelEntity> RetriveLabel(long labelId)
        {
            try
            {
                return this.ilabelRL.RetriveLabel(labelId);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
