INSERT INTO lessons (id, information, name, teacher)
VALUES (1, 'First lesson', 'First', 'First teacher'),
       (2, 'Second lesson', 'Second', 'Second teacher'),
       (3, 'Third lesson', 'Third', 'Third teacher'),
       (4, '4', '4', '4'),
       (5, '5', '5', '5'),
       (6, '6', '6', '6'),
       (7, '7', '7', '7'),
       (8, '8', '8', '8'),
       (9, '9', '9', '9'),
       (10, '10', '10', '10'),
       (11, '11', '11', '11');

INSERT INTO lesson_time (id, time, lesson_id)
VALUES (1, '09:00:00', 1),
       (2, '10:45:00', 2),
       (3, '13:00:00', 3),
       (4, '09:00:00', 2),
       (5, '10:45:00', 1),
       (6, '13:00:00', 3);

INSERT INTO days (id, date, information)
VALUES (1, '2022-09-01', 'First day'),
       (2, '2022-09-02', 'Second day'),
       (3, '2022-09-03', 'Third day');

INSERT INTO days_lessons (day_id, lessons_and_times_id)
VALUES (1, 1),
       (1, 2),
       (2, 1),
       (2, 6),
       (2, 3),
       (3, 4),
       (3, 5),
       (3, 6);

INSERT INTO news (id, date_time_of_create, message, need_to_send, pictures)
VALUES (1, NOW(), 'Is sent message', 0, ''),
       (2, NOW(), 'Not sent message', 1, '');

INSERT INTO passwords (id, value)
VALUES (1, 'admin');

INSERT INTO users (id, chat_id, login, name, role, token, password_id)
VALUES (1, 1234567, 'admin', 'admin', 1, '', 1);

INSERT INTO note (id, description, day_id, user_id)
VALUES (1, 'Admin note', 1, 1);