INSERT INTO lessons (id, name, information, teacher)
VALUES (1, 'Lesson (1)', 'Info (1)', 'Teacher (1)'),
       (2, 'Lesson (2)', 'Info (2)', 'Teacher (2)'),
       (3, 'Lesson (3)', 'Info (3)', 'Teacher (3)'),
       (4, 'Lesson (4)', 'Info (4)', 'Teacher (4)'),
       (5, 'Lesson (5)', 'Info (5)', 'Teacher (5)'),
       (6, 'Lesson (6)', 'Info (6)', 'Teacher (6)'),
       (7, 'Lesson (7)', 'Info (7)', 'Teacher (7)'),
       (8, 'Lesson (8)', 'Info (8)', 'Teacher (8)'),
       (9, 'Lesson (9)', 'Info (9)', 'Teacher (9)'),
       (10, 'Lesson (10)', 'Info (10)', 'Teacher (10)'),
       (11, 'Lesson (11)', 'Info (11)', 'Teacher (11)');

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

INSERT INTO news (id, date_time_of_create, message, need_to_send, pictures, lesson_id)
VALUES (1, NOW(), 'Is sent message (1)', 0, 'test|', NULL),
       (2, DATE_SUB(NOW(), INTERVAL 1 DAY), 'Not sent message (2)', 1, 'test|test|', 1),
       (3, DATE_SUB(NOW(), INTERVAL 2 DAY), 'Is sent message (3)', 0, 'test|test|test|', NULL),
       (4, DATE_SUB(NOW(), INTERVAL 3 DAY), 'Not sent message (4)', 1, '', 2),
       (5, DATE_SUB(NOW(), INTERVAL 4 DAY), 'Is sent message (5)', 0, '', NULL),
       (6, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Not sent message (6)', 1, '', 3),
       (7, DATE_SUB(NOW(), INTERVAL 6 DAY), 'Is sent message (7)', 0, '', NULL),
       (8, DATE_SUB(NOW(), INTERVAL 7 DAY), 'Not sent message (8)', 1, '', 4),
       (9, DATE_SUB(NOW(), INTERVAL 8 DAY), 'Is sent message (9)', 0, '', NULL),
       (10, DATE_SUB(NOW(), INTERVAL 9 DAY), 'Not sent message (10)', 1, '', 5),
       (11, DATE_SUB(NOW(), INTERVAL 10 DAY), 'Is sent message (11)', 0, '', NULL),
       (12, DATE_SUB(NOW(), INTERVAL 11 DAY), 'Not sent message (12)', 1, '', 6),
       (13, DATE_SUB(NOW(), INTERVAL 12 DAY), 'Is sent message (13)', 0, '', NULL),
       (14, DATE_SUB(NOW(), INTERVAL 13 DAY), 'Not sent message (14)', 1, '', 7),
       (15, DATE_SUB(NOW(), INTERVAL 14 DAY), 'Is sent message (15)', 0, '', NULL),
       (16, DATE_SUB(NOW(), INTERVAL 15 DAY), 'Not sent message (16)', 1, '', 8),
       (17, DATE_SUB(NOW(), INTERVAL 16 DAY), 'Is sent message (17)', 0, '', NULL),
       (18, DATE_SUB(NOW(), INTERVAL 17 DAY), 'Not sent message (18)', 1, '', 9),
       (19, DATE_SUB(NOW(), INTERVAL 18 DAY), 'Is sent message (19)', 0, '', NULL),
       (20, DATE_SUB(NOW(), INTERVAL 19 DAY), 'Not sent message (20)', 1, '', 10),
       (21, DATE_SUB(NOW(), INTERVAL 20 DAY), 'Is sent message (21)', 0, '', NULL);

INSERT INTO passwords (id, value)
VALUES (1, 'admin');

INSERT INTO users (id, chat_id, login, name, role, token, password_id)
VALUES (1, 1234567, 'admin', 'admin', 1, '', 1);

INSERT INTO note (id, description, day_id, user_id)
VALUES (1, 'Admin note', 1, 1);