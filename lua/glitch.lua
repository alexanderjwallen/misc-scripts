#!/usr/bin/lua
args = {...}
if not args[1] then
	io.stdout:write("Please enter file name: ")
	file = io.read()
else
	file = args[1]
end

f = io.open(file,"rb")
dat = f:read("*a") 
f:close() 

local e = dat:find(string.char(33,255))-- find color table end
if not e then
	e = dat:find(string.char(33,249))
end


f = io.open("out.gif","wb")
f:write(dat:sub(1,13))-- write gif header
for i = 1,e - 14 do-- write randomized color table
	f:write(string.char(math.random(0,255)))
end
f:write(dat:sub(e,-1))-- write the rest of the gif data
f:close(
