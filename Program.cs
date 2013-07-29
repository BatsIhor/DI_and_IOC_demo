using System;
using System.Collections.Generic;

namespace DI_and_IOC_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            WithoutDI withoutDi = new WithoutDI();

            withoutDi.UseRepo();

            Console.WriteLine();
            Console.WriteLine("Next is our second class wich can work with different repositories.");
            Console.WriteLine();


            WithDI withDbDi = new WithDI(new DbRepository());
            WithDI withFileDi = new WithDI(new FileRepository());

            withDbDi.UseRepo();
            withFileDi.UseRepo();

            Console.WriteLine();
            Console.WriteLine("IOC demo");

            IOC ioc = new IOC();

            ioc.Register("someRepo", new DbRepository());
            ioc.Register("OtherRepo", new FileRepository());

            Console.ReadLine();
        }
    }

    /// <summary>
    /// Class without Dependency Injection
    /// </summary>
    class WithoutDI
    {
        DbRepository _repository = new DbRepository();

        public void UseRepo()
        {
            // Dummy logic.

            Console.WriteLine("I am " + this.GetType().Name + " and I work with " + _repository.GetType().Name);
        }
    }

    /// <summary>
    /// Class with Dependency Injection
    /// </summary>
    class WithDI
    {
        private IRepository _repository;

        public WithDI(IRepository repository)
        {
            _repository = repository;
        }

        public void UseRepo()
        {
            // Dummy logic.

            Console.WriteLine("I am " + this.GetType().Name + " and I work with " + _repository.GetType().Name);
        }
    }

    /// <summary>
    /// Class with Dependency Injection
    /// </summary>
    class WithIOC
    {
        private IRepository _repository;

        public WithIOC(IOC ioc)
        {
            _repository = (IRepository)ioc.Resolve("someRepo");
        }

        public void UseRepo()
        {
            // Dummy logic.

            Console.WriteLine("I am " + this.GetType().Name + " and I work with " + _repository.GetType().Name + "recieved from IOC");
        }
    }

    /// <summary>
    /// My as simple as possible IOC 
    /// </summary>
    class IOC
    {
        Dictionary<string, object> dependencies = new Dictionary<string, object>();

        public void Register(string name, object obj)
        {
            dependencies.Add(name, obj);
        }

        public object Resolve(string name)
        {
            return dependencies[name];
        }
    }

    #region Repository
    internal interface IRepository
    {
        void Create();
        void Read();
        void Update();
        void Delete();
    }

    class DbRepository : IRepository
    {
        public void Create() { }
        public void Read() { }
        public void Update() { }
        public void Delete() { }
    }

    class FileRepository : IRepository
    {
        public void Create() { }
        public void Read() { }
        public void Update() { }
        public void Delete() { }
    }
    #endregion
}
