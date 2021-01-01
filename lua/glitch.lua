#!/usr/bin/lua
args = {...}
if not args[1] then
	io.stdout:write("Please enter file name: ")
	file = io.read()
else
	file = args[1]
end

f = io.open(file,"rb") --open gif
dat = f:read("*a") --read all gif data
f:close() --close gif
io.stdout:write("Glitching "..file)
tdat = {}
local i = 0
local mod = math.floor(#dat/4)

for c in dat:gmatch(".") do --convert gif data(string) to a table
	if i % mod == 0 then io.stdout:write(".") end
	table.insert(tdat,c) 
	i = i + 1
end

local e
for i = 1,#tdat do --find end of color table
	if string.byte(tdat[i]) == 33 then
		if string.byte(tdat[i + 1]) == 255 then
			e = i
			break
		elseif string.byte(tdat[i + 1]) == 249 then
			e = i
			break
		end
	end
end
math.randomseed(os.time())
for i = 13,e-1 do--randomize color table
	tdat[i] = string.char(math.random(0,255))
end

outfile = file:gsub("%.gif","-glitched.gif")
f = io.open(outfile,"wb")
f:write(table.concat(tdat))
f:close()
print("\ndone!")