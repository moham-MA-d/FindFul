
--Get user roles by username
SELECT u.UserName, r.Name AS Role FROM Users u, UserRoles ur, Roles r
WHERE u.Id = ur.UserId
AND r.Id = ur.RoleId
AND u.UserName = 'teresa';
;


