using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Schedulerer.Task
{
    [System.Serializable]
    public class TaskSchedulerer
    {
        //���� �½�ũ �۵� ������ �ش� ���Ͽ� Ȱ���ϴ� ��� ������Ʈ�� �˻��Ѵ�.
        //�½�ũ ��������Ʈ�� �ð��뺰�� �и��ϸ� �� ���� ȣ���ϴ� ������Ʈ�� �� �ٿ��� ���� ������ �� �� �ִ�.
        //ex) ���� -> ���� -> �ɾ� -> �������� 6�ð�( 72 ƽ ������ ���� )�Ͽ� ���� ƽ�� ���� �ش� �ð����� ��������Ʈ�� ����ϴ� ��.


        private delegate void TaskUpdate(int tick);
        private delegate void TaskOverwrite(int id, Task task);

        private TaskUpdate taskUpdate;

        // ( ������ ������ ���� ���� )
        [SerializeField]
        private List<Task> disabledScheduleTaskPool = new List<Task>();

        //����μ��� Task����� �����۵��ϴ��� �����ϱ� ���Ͽ� �߰��� ��
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
            //���� Task����� �����۵��� Ȯ���ϸ� enabled Pool�� �������� �������̹Ƿ�, disabled Pool�� üũ��.
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
