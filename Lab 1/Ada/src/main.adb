with Ada.Text_IO;

procedure Main is

   can_stop : boolean := false;
   pragma Atomic(can_stop);

   task type break_thread;
   task type main_thread(id, step: Integer);

   task body break_thread is
   begin
      delay 1.0;
      can_stop := true;
   end break_thread;

   task body main_thread is
      sum : Long_Long_Integer := 0;
      current : Long_Long_Integer := 0;
   begin
      loop
      current := current + Long_Long_Integer(step);
         sum := sum + current;
         exit when can_stop;
      end loop;
      delay 1.0;

      Ada.Text_IO.Put_Line("task id:" & id'Img & "; sum:" & sum'Img & "; count:" & Long_Long_Integer'Image(current / Long_Long_Integer(step)));
   end main_thread;

   b1 : break_thread;
   t1 : main_thread(1, 3);
   t2 : main_thread(2, 2);
   t3 : main_thread(3, 4);
   t4 : main_thread(4, 7);
begin
   null;
end Main;
