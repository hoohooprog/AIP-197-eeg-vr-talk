clc
clear all
data = load_xdf('C:\Recordings\AIP_present.xdf');
%size gives the array of ints of struct from 1 to X
unity_stamp_size = size(data{1}.time_stamps)
%find the index when game ends
game_end_stamp_index = find([data{1}.time_series] == "end of data rec ")

% 0-shift unity time-stamps
data{1}.time_stamps = data{1}.time_stamps - data{1}.time_stamps(unity_stamp_size(1));

unity_duration = data{1}.time_stamps(game_end_stamp_index) - data{1}.time_stamps(unity_stamp_size(1))  
 
% % subtract by start of begin of recording from time-stamp to shift start to
% % 0s
data{2}.time_stamps = data{2}.time_stamps - data{2}.segments.t_begin;
%  
% figure;
% plot(data{1}.time_stamps, data{1}.time_series)
% hold on
figure;
plot(data{2}.time_stamps, data{2}.time_series(1,:))
hold all
plot(data{1}.time_stamps(game_end_stamp_index),data{2}.time_series(1),'o', 'LineWidth',2)
%plot(streams{2}.time_stamps,'LineWidth',2)
% matlab as client, unity as server
% tcpipClient = tcpip('127.0.0.1',55001,'NetworkRole','Client');
% set(tcpipClient,'Timeout',30);
% fopen(tcpipClient);
% a='yah!! we could make it';
% fwrite(tcpipClient,a);
% fclose(tcpipClient);



%https://github.com/sccn/labstreaminglayer/wiki/Tutorial-1.-Getting-started-with-LSL-single-stream

% start recording from eeg
% start unity
% make sure to exclude eeg_marker before starting labrecorder
% start labrecorder
% send signal from matlab to fade to black and that sends marker to mark
% end of game eeg signal
% stop labrecorder