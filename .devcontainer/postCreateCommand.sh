#!/bin/sh
script_user=`whoami`
script_dir=$(realpath "$(dirname "$0")")

echo "USER:" ${script_user}
echo "DIR:" ${script_dir}
echo

# add xunit3 template
dotnet new install xunit.v3.templates
