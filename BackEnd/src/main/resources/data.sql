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
VALUES (1, '01-09-2022', 'First day'),
       (2, '02-09-2022', 'Second day'),
       (3, '03-09-2022', 'Third day');

INSERT INTO days_lessons
VALUES (1, 1),
       (1, 2),
       (2, 1),
       (2, 1),
       (2, 3),
       (3, 4),
       (3, 5),
       (3, 6);

INSERT INTO news
VALUES (1, 'Is sent message', 0),
       (2, 'Not sent message', 1);