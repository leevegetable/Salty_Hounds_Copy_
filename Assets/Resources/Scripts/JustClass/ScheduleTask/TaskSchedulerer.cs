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
        //�� �� ���, ������Ʈ�� Ȱ�� �ð��밡 �������� ��, �� �ð����� ��������Ʈ�� ���� ���� �������Ѿ��Ѵ�.
        //�׷��� ���� ���, �̺�Ʈ�� ���� �ð� ��ŵ�� �߻��� ���, Ȱ���� ���� �ʱ� ����
        //���� �� �ð����� ��������Ʈ�� ��� ������ ���, �ռ� �ð��뿡�� �½�ũ�� �۵����� ��� �Ŀ� �ð����� �½�ũ ���� ã�Ƽ� �������ִ� �ڵ尡 �ʿ��ϴ�.
        //Work���� �˻��Ͽ� bool���� ���Ϲ޾� ��������Ʈ ������ �����ϴ°� ������ �� �� �ϴ�.

        private delegate void TaskExecute(int tick);
        private delegate void TaskOverwrite(int id, Task task);

        private TaskExecute taskExecute;
        [SerializeField]
        private List<Task> disabledScheduleTaskPool = new List<Task>();

        //����μ��� Task����� �����۵��ϴ��� �����ϱ� ���Ͽ� �߰��� �� ( ������ ������ ���� ���� )
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
            //���� Task����� �����۵��� Ȯ���ϸ� enabled Pool�� �������� �������̹Ƿ�, disabled Pool�� üũ��.
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
