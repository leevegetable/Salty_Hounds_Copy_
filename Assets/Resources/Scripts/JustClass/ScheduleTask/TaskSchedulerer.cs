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
        //단 이 경우, 오브젝트의 활동 시간대가 겹쳐있을 때, 각 시간대의 델리게이트에 같은 값을 참조시켜야한다.
        //그렇지 않을 경우, 이벤트에 의한 시간 스킵이 발생할 경우, 활동을 하지 않기 때문
        //또한 각 시간대의 델리게이트에 모두 삽입할 경우, 앞선 시간대에서 태스크가 작동했을 경우 후열 시간대의 태스크 또한 찾아서 삭제해주는 코드가 필요하다.
        //Work으로 검사하여 bool값을 리턴받아 델리게이트 참조를 해제하는걸 만들어야 할 듯 하다.

        private delegate void TaskExecute(int tick);
        private delegate void TaskOverwrite(int id, Task task);

        private TaskExecute taskExecute;
        [SerializeField]
        private List<Task> disabledScheduleTaskPool = new List<Task>();

        //현재로서는 Task기능이 정상작동하는지 추적하기 위하여 추가한 것 ( 가비지 데이터 생성 방지 )
        [SerializeField]
        private List<Task> enabledScheduleTaskPool = new List<Task>();
        private List<TaskOverwrite> listOverwrite = new List<TaskOverwrite>();
        public bool isOverwrite = false;
        public void Execute(int tick)
        {
            if (taskExecute == null) return;
            taskExecute(tick);
        }

        public void Add(int id, int startTick, int endTick, HourSchedule work)
        {
            Task task = null;
            if (disabledScheduleTaskPool.Count == 0)
            {
                task = new Task(this, id, startTick, endTick, work);
                
                taskExecute += task.Execute;
                enabledScheduleTaskPool.Add(task);
                Overwrite(id, task);
            }
            else
            {
                task = disabledScheduleTaskPool[0];
                disabledScheduleTaskPool.RemoveAt(0);
                task.setTask(id, startTick, endTick, work);
                taskExecute += task.Execute;
                enabledScheduleTaskPool.Add(task);
                Overwrite(id, task);

            }
        }

        private bool Overwrite(int id, Task task)
        {
            if (listOverwrite.Count - 1 < id)
            {
                listOverwrite.Add(new TaskOverwrite(task.overWriteTask));
                return false;
            }
            else
            {
                listOverwrite[id] += task.overWriteTask;
                listOverwrite[id](id, task);
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
                taskExecute -= task.Execute;
                enabledScheduleTaskPool.Remove(task);
                listOverwrite[task.TimeSchedulererID] -= task.overWriteTask;
                task.setNull();
                disabledScheduleTaskPool.Add(task);
            }

        }


    }

}
