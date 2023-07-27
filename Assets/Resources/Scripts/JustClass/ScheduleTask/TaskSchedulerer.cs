using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Schedulerer.Task
{
    [System.Serializable]
    public class TaskSchedulerer
    {
        //현재 태스크 작동 구조는 해당 요일에 활동하는 모든 오브젝트를 검사한다.
        //태스크 델리게이트를 시간대별로 분리하면 한 번에 호출하는 오브젝트를 더 줄여서 더욱 가볍게 할 수 있다.
        //ex) 오전 -> 오후 -> 심야 -> 새벽으로 6시간( 72 틱 단위로 분할 )하여 현재 틱에 따라 해당 시간대의 델리게이트만 사용하는 것.


        private delegate void TaskUpdate(int tick);
        private delegate void TaskOverwrite(int id, Task task);

        private TaskUpdate taskUpdate;

        // ( 가비지 데이터 생성 방지 )
        [SerializeField]
        private List<Task> disabledScheduleTaskPool = new List<Task>();

        //현재로서는 Task기능이 정상작동하는지 추적하기 위하여 추가한 것
        [SerializeField]
        private List<Task> enabledScheduleTaskPool = new List<Task>();
        private Dictionary<int,TaskOverwrite> DictOverwrite = new Dictionary<int, TaskOverwrite>();
        public bool isOverwrite = false;
        public void Update(int tick)
        {
            if (taskUpdate == null) return;
            taskUpdate(tick);
        }

        public void Add(int id, int startTick, int endTick, HourSchedule work)
        {
            Task task = null;
            if (disabledScheduleTaskPool.Count == 0)
            {
                Debug.Log("New Task Updated");
                task = new Task(this, id, startTick, endTick, work);
                
                taskUpdate += task.Execute;
                enabledScheduleTaskPool.Add(task);
                Overwrite(id, task);
            }
            else
            {
                task = disabledScheduleTaskPool[0];
                disabledScheduleTaskPool.RemoveAt(0);
                task.setTask(id, startTick, endTick, work);
                taskUpdate += task.Execute;
                enabledScheduleTaskPool.Add(task);
                Overwrite(id, task);

            }
        }

        private bool Overwrite(int id, Task task)
        {
            if (!DictOverwrite.ContainsKey(id))
            {
                DictOverwrite.Add(id,new TaskOverwrite(task.overWriteTask));
                return false;
            }
            else
            {
                DictOverwrite[id] += task.overWriteTask;
                DictOverwrite[id](id, task);
            }
            if (!isOverwrite)
            {
                return false;
            }

            else
            {
                isOverwrite = false;
                return true;
            }

        }

        public void Clear()
        {
            if (DictOverwrite.Count < 0) return;
            for (int i = enabledScheduleTaskPool.Count - 1; i >= 0; --i)
            {
                Remove(enabledScheduleTaskPool[i]);
            }
        }

        public void Remove(Task task)
        {
            //추후 Task기능의 정상작동을 확인하면 enabled Pool을 실행하지 않을것이므로, disabled Pool만 체크함.
            if (!disabledScheduleTaskPool.Contains(task))
            {
                taskUpdate -= task.Execute;
                enabledScheduleTaskPool.Remove(task);
                DictOverwrite[task.TimeSchedulererID] -= task.overWriteTask;
                task.setNull();
                disabledScheduleTaskPool.Add(task);
            }

        }


    }

}
