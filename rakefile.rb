require 'rubygems'
require 'albacore'
require 'fileutils'

include FileUtils

task :default => [:all]

desc "Rebuild solution"
msbuild :build do |msb, args|
    msb.properties :configuration => :Debug
    msb.verbosity = :minimal
    msb.targets :Rebuild
    msb.solution = "TheY.sln"
end

desc "Run everything!"
task :all => [:build]

desc "test using nunit console"
nunit :test => :build do |nunit|
    nunit.command = "packages/NUnit.Runners.2.6.2/tools/nunit-console.exe"
    nunit.assemblies "Tests/bin/Debug/Tests.dll"
end

