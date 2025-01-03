# Udemy-Like Course Purchase Project

This project aims to provide a platform where users can browse and filter courses, purchase them, and where admins can manage the system. The project is built using modern technologies and adheres to best software development practices.

## Features

- **Course Listing**: View all courses on the homepage.
- **Filtering**: Filter courses by category and price range.
- **User Registration**: Register to the system and receive a one-time discount coupon via RabbitMQ after registration.
- **Redis-Backed Coupon and Cart System**:
  - Coupons are stored in the Redis database and deleted after the order is completed.
  - Courses added to the cart are stored in Redis.
- **Admin Panel**:
  - Real-time statistics tracking on the dashboard using SignalR.
  - Total courses, total sales, total instructors, and a list of orders.
  - Assign roles to users.

## Technologies and Tools

- **Backend**: ASP.NET MVC (.NET 9.0)
- **Database**:
  - Entity Framework and SQL Server.
  - Redis (In-Memory).
- **Message Queueing**: RabbitMQ (with MassTransit library).
- **Real-Time Tracking**: SignalR.
- **User Authentication**: ASP.NET Identity.
- **Validation**: FluentValidation.
- **Design Pattern**: Repository Design Pattern.
- **Principles**: Adherence to SOLID principles.
- **Frontend**: HTML, CSS, JavaScript, jQuery, Bootstrap.







https://github.com/user-attachments/assets/84e4d5ae-3689-4dd6-a313-87e8f6c89c7b

