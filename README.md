# i-cloud
i need a repository from network to store my code ,then after 10 years it'll become my beautiful memory.
i need a repository from network to store my code ,then after 10 years it'll become my beautiful memory
i need a repository from network to store my code ,then after 10 years it'll become my beautiful memory

Basic Overview of the Asynchronous Model
The core of async programming are the Task and Task<T> objects, which model asynchronous operations. They are supported by the async and await keywords. The model is fairly simple in most cases:
For I/O-bound code, you await an operation which returns a Task or Task<T> inside of an async method.
For CPU-bound code, you await an operation which is started on a background thread with the Task.Run method.
The await keyword is where the magic happens. It yields control to the caller of the method that performed await, and it ultimately allows a UI to be responsive or a service to be elastic.
There are other ways to approach async code than async and await outlined in the TAP article linked above, but this document will focus on the language-level constructs from this point forward.
