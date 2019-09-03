using System.Collections.Generic;
using System.Linq;

namespace MaximovInk.FlatinyEngine.Core.ProcessManagment
{
    public static class ProcessManager
    {
        private static List<OrderedProcess<IStartHandler>> startHandlers = new List<OrderedProcess<IStartHandler>>();
        private static List<OrderedProcess<IEndHandler>> endHandlers = new List<OrderedProcess<IEndHandler>>();
        private static List<OrderedProcess<IUpdateHandler>> updateHandlers = new List<OrderedProcess<IUpdateHandler>>();
        private static List<OrderedProcess<IRenderHandler>> renderHandlers = new List<OrderedProcess<IRenderHandler>>();

        public static void RegisterStartHandler(IStartHandler handler, int priority)
        {
            startHandlers.RegisterHandler(handler, priority);
            ProcessUtilites.SortOrder(ref startHandlers);
            handler.Start();
        }

        public static void RegisterUpdateHandler(IUpdateHandler handler, int priority)
        {
            updateHandlers.RegisterHandler(handler, priority);
            ProcessUtilites.SortOrder(ref updateHandlers);
        }

        public static void RegisterRenderHandler(IRenderHandler handler, int priority)
        {
            renderHandlers.RegisterHandler(handler, priority);
            ProcessUtilites.SortOrder(ref renderHandlers);
        }

        public static void RegisterEndHandler(IEndHandler handler, int priority)
        {
            endHandlers.RegisterHandler(handler, priority);
            ProcessUtilites.SortOrder(ref endHandlers);
        }

        public static void RemoveStartHandler(IStartHandler handler)
        {
            startHandlers.RemoveHandler(handler);
            ProcessUtilites.SortOrder(ref startHandlers);
        }

        public static void RemoveUpdateHandler(IUpdateHandler handler)
        {
            updateHandlers.RemoveHandler(handler);
            ProcessUtilites.SortOrder(ref updateHandlers);
        }

        public static void RemoveRenderHandler(IRenderHandler handler)
        {
            renderHandlers.RemoveHandler(handler);
            ProcessUtilites.SortOrder(ref renderHandlers);
        }

        public static void RemoveEndHandler(IEndHandler handler)
        {
            handler.End();
            endHandlers.RemoveHandler(handler);
            ProcessUtilites.SortOrder(ref endHandlers);
        }

        public static void Update(float deltaTime)
        {
            for (int i = 0; i < updateHandlers.Count; i++)
            {
                updateHandlers[i].process.Update(deltaTime);
            }
        }

        public static void Render(float deltaTime)
        {
            for (int i = 0; i < renderHandlers.Count; i++)
            {
                renderHandlers[i].process.Render(deltaTime);
            }
        }

    }

    public struct OrderedProcess<T> where T : IHandler
    {
        public T process;
        public int priority;
    }

    public static class ProcessUtilites
    {
        public static void RegisterHandler<T>(this List<OrderedProcess<T>> orderedProcesses, T process, int priority) where T : IHandler
        {
            orderedProcesses.Add(new OrderedProcess<T> { process = process , priority = priority });
            orderedProcesses = orderedProcesses.Where(n => n.process != null).OrderByDescending(n => n.priority).ToList();
        }

        public static void SortOrder<T>(ref List<OrderedProcess<T>> orderedProcesses) where T : IHandler
        {
            orderedProcesses = orderedProcesses.Where(n => n.process != null).OrderByDescending(n => n.priority).ToList();
        }

        public static void RemoveHandler<T>(this List<OrderedProcess<T>> orderedProcesses, T process) where T : IHandler
        {
            orderedProcesses.Remove(orderedProcesses.First(n => n.process.Equals(process)));
        }
    }
}
