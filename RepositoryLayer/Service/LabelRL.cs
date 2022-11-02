﻿using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;

        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public bool CreateLabel(long noteId, long userId, string labelName)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(e => e.UserId == userId);
                if (result != null)
                {
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.NoteID = noteId;
                    labelEntity.UserId = userId;
                    labelEntity.LabelName = labelName;

                    fundooContext.LabelTable.Add(labelEntity);  
                    int saveResult = fundooContext.SaveChanges();
                    if(saveResult > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
