using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Schedulerer.Task
{
    [System.Serializable]
    public class Task
    {
        public TaskSchedulerer Timeline;
        public int TimeSchedulererID = 0;
        public int StartTick = 0;
        public int EndTick = 0;
        /// <summary>
        /// HourSchedule Class Only
        /// </summary>
        public HourSchedule Work;
        public Task(TaskSchedulerer timeline, int timeSchedulererID, int startTick, int endTick, HourSchedule work)
        {
            Timeline = timeline;
            TimeSchedulererID = timeSchedulererID;
            StartTick = startTick;
            EndTick = endTick;
            Work = work;
        }

        public void setTask(int timeSchedulererID, int startTick, int endTick, HourSchedule work)
        {
            TimeSchedulererID = timeSchedulererID;
            StartTick = startTick;
            EndTick = endTick;
            Work = work;
        }

        public void setNull()
        {
            TimeSchedulererID = 0;
            StartTick = 0;
            EndTick = 0;
            Work = null;
        }

        public void Execute(int tick)
        {
            if (StartTick <= tick && tick <= EndTick)
            {
                if (tick > EndTick) returnDisablePool();
                Work.update(TimeSchedulererID);
            }
            else
            {
                return;
            }
        }

        public void overWriteTask(int id, Task task)
        {
            if (task == null||task == this) return;
            //task가 먼저 혹은 동시 시작하여, 종료시점이 자신의 시작지점을 넘길경우
            //me                 ☆■■■■■■■■■★
            //task         ☆? ~ ☆?
            //task               ★?            ~             ★?
            if (task.StartTick <= StartTick && task.EndTick >= StartTick)
            {
                //종료 시점이 자신의 종료시점보다 작을경우, 본인의 시작지점을 조정
                //me                 ☆■■■■■■■■■★
                //task         ☆? ~ ☆?
                //task               ★?  ~    ~     ~ ★?
                if (task.EndTick < EndTick)
                {
                    StartTick = task.EndTick + 1;
                }
                //종료시점이 자신을 넘을경우, 자신 삭제.
                else
                {
                    returnDisablePool();
                }
            }
            //task가 자신의 가운데에 있을 경우
            //me                 ☆■■■■■■■■■★
            //task                 ~ ☆?
            //task                                ~★?
            else if (task.StartTick > StartTick && task.EndTick < EndTick)
            {
                int tempEnd = EndTick;
                EndTick = task.StartTick - 1;
                Timeline.Add(id, task.EndTick + 1, tempEnd, Work);
                //분할 후 복제.

            }
            else if (task.StartTick <= EndTick && task.EndTick >= EndTick)
            {
                if (task.StartTick < StartTick)
                {
                    EndTick = task.StartTick - 1;
                }
                else
                {
                    returnDisablePool();
                }
            }
            else
            {
                return;
            }
            Timeline.isOverwrite = true;
            return;
        }

        private void returnDisablePool()
        {
            if (Timeline == null) return;
            setNull();
            Timeline.Remove(this);
        }
    }
}

