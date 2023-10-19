# 001_CWRK-Software-design-and-implementation-exercise

## Assignment Details ## 
 

### Introduction ### 

 

A Cruise2Holiday administration system must be designed and implemented (using C# and Visual Studio) for use by an administrator.  
A cruise is a holiday aboard a ship that visits a number of ports.  At each port, day trips may be arranged for the passengers. 
At each port visited, a passenger may take a maximum of one trip for each day the ship is in port.  
Each trip has a cost.  All passengers are allowed two trips at the cost of the cruise, any additional trips must be paid for in addition to the cruise cost. 
The system must allow the administrator to perform the operations listed below.


### Operations###

Add a cruise to the system. A cruise has a name but is identified by an identifier.
Remove a cruise.
Add a port, remove a port.
Add and remove trips at a specific port.
Add and remove ports from a specific cruise.
Add and remove passengers to a cruise. A passenger has a name and a passport number. Passport numbers must be unique within the application.
Add and remove passengers to a trip.
A passenger cannot be booked on a trip at a port that is not on the passenger’s cruise.
Note that, like real-world requirements, the above requirements may require you to use your judgment to resolve inconsistencies or for completeness. You may also include other additional features if you wish. 

Whenever an attempt is made to perform an operation and a state invariant does not hold, an application exception should be thrown.

### Application Design ###

The application should be designed in an object-oriented pattern. Three layers, 1 – business logic, 2 – data storage, and 3 - user interface, should be clearly specified. There should also be a well-defined separation between the layers so that, for example, there should be no user interface code or data storage code in the business logic.   

### Data Storage Design ###

All of the application data must be presented in XML format. You need to design an XML schema to describe the cruise information. Each XML document must be well-formed. 

### Interface Design ###

Either a console or GUI (WPF) based interface can be chosen for the application.

### Assessment ###

It is important to appreciate that the evidence for good design must be present both in the report and in the implementation.  The time allowed for this assignment does not permit a complete implementation and nor is a complete implementation required for full marks.  Elements of the implementation may be mocked up, providing the design is clear. Object types should model the real world from the application domain according to object-oriented design principles.  Code should implement design described in the associated report, make appropriate use of .NET classes, be well structured and adhere to good coding guidelines.

## Mark Scheme ##

### Report Requirements: ###

You are to write a report about your design that will include the following sections: 

1. Summary of  your system design and implementation (4).

2. Logical description of object classes (6).

3. Logical description of relationships between objects (6).

4. Business logic description (6).

5. Data storage and structure design (6).

6. User interaction design (4).

7. Translation to and from data storage and UI to business logic objects (4).

8. Testing report. Illustrate the test strategies and explaining methods and techniques used for selecting the minimal size of test cases driven by different testing strategies (10).

9. Evaluation of implementation (4).

### Software Requirements: ###

Your software should include the following:

1. Successfully un-zip and compile your submission without any bugs (5).

2. Successfully execute No.1 operations (3).

3. Successfully execute No. 2 operation (2).

4. Successfully execute No. 3 operations (2).

5. Successfully execute No. 4 operation (3).

6. Successfully execute No. 5 operation (3).

7. Successfully execute No. 6 operation (4).

8. Successfully execute No. 7 operation (2).

9. Successfully execute No. 8 operation (4).

10. Successfully read and write XML data on the basis of the defined XML schema (10).

11. UI design (preventing incorrect input and show correct results etc.) (10).

12. New features or innovations (2). 

## Submission ##

1. The report must be in MS Word format.

2. Clean (Build menu >Clean) the solution and then Zip the solution (use 7-Zip) into a single compressed file.

4. Submit both the compressed solution file and report to Canvas.
