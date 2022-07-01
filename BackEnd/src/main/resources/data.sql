INSERT INTO passwords
VALUES (1, 'qwerty'),
       (2, '123456'),
       (3, 'q1w2e3');

INSERT INTO users
VALUES (1, NULL, 'vova@gmail.com', 'Vova', 1, NULL, 1),
       (2, NULL, 'dima@gmail.com', 'Dima', 0, NULL, 2),
       (3, NULL, 'petya@gmail.com', 'Petya', 0, NULL, 3);

INSERT INTO lessons
VALUES (1, 'First lesson', 'First', 'First teacher'),
       (2, 'Second lesson', 'Second', 'Second teacher'),
       (3, 'Third lesson', 'Third', 'Third teacher');

INSERT INTO lesson_time
VALUES (1, '09:00:00', 1),
       (2, '10:45:00', 2),
       (3, '13:00:00', 3),
       (4, '09:00:00', 2),
       (5, '10:45:00', 1),
       (6, '13:00:00', 3);

INSERT INTO days
VALUES (1, '2022-09-01', 'First day'),
       (2, '2022-09-02', 'Second day'),
       (3, '2022-09-03', 'Third day');

INSERT INTO days_lessons
VALUES (1, 1),
       (1, 2),
       (2, 1),
       (2, 1),
       (2, 3),
       (3, 4),
       (3, 5),
       (3, 6);

INSERT INTO note
VALUES (1, 'Vova note', 1, 1),
       (2, 'Dima note', 1, 2);

INSERT INTO news
VALUES (1, 'Is sent message', 0),
       (2, 'Not sent message', 1);