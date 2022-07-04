
#include<iostream>
#include<locale>
#include"omp.h"

using namespace std;

const int arr_size = 20000;
const int arr_size2 = 20000;
int arr[arr_size][arr_size2];

void init_arr();
long long part_sum(int, int, int, int, int);
long long part_min(int, int, int, int, int);

int main() {
    init_arr();

    omp_set_nested(1);
    double t1 = omp_get_wtime();
#pragma omp parallel sections
    {
#pragma omp section
        {   
        cout << "Result 1 = " << part_sum(0, arr_size, 0, arr_size2, 1) << endl;
        cout << "Result 2 = " << part_sum(0, arr_size, 0, arr_size2, 2) << endl;
        cout << "Result 3 = " << part_sum(0, arr_size, 0, arr_size2, 3) << endl;
        cout << "Result 4 = " << part_sum(0, arr_size, 0, arr_size2, 4) << endl;
        cout << "Result 8 = " << part_sum(0, arr_size, 0, arr_size2, 8) << endl;
        cout << "Result 16 = " << part_sum(0, arr_size, 0, arr_size2, 16) << endl;
        }

#pragma omp section
        {
            cout << "Result 1 = " << part_min(0, arr_size, 0, arr_size2, 1) << endl;
            cout << "Result 2 = " << part_min(0, arr_size, 0, arr_size2, 2) << endl;
            cout << "Result 3 = " << part_min(0, arr_size, 0, arr_size2, 3) << endl;
            cout << "Result 4 = " << part_min(0, arr_size, 0, arr_size2, 4) << endl;
            cout << "Result 8 = " << part_min(0, arr_size, 0, arr_size2, 8) << endl;
            cout << "Result 16 = " << part_min(0, arr_size, 0, arr_size2, 16) << endl;
        }
    }


    double t2 = omp_get_wtime();
    cout << "Total time - " << t2 - t1 << " seconds" << endl;
    return 0;
}

void init_arr() {
    for (int i = 0; i < arr_size; i++) {
        for (int j = 0; j < arr_size2; j++) {
            arr[i][j] = i + j;
        }

    }


}

long long part_sum(int start_index, int finish_index, int start_index2, int finish_index2, int num_threads) {
    long long sum = 0;
    double t1 = omp_get_wtime();
#pragma omp parallel for reduction(+:sum) num_threads(num_threads)  
    for (int i = start_index; i < finish_index; i++) {
        for (int j = start_index2; j < finish_index2; j++) {
            sum += arr[i][j];
        }

    }

    double t2 = omp_get_wtime();
    cout << "Total sum of all elements, " << "Thread - " << num_threads << ", time -  " << t2 - t1 << " seconds" << endl;

    return sum;
}

long long part_min(int start_index, int finish_index, int start_index2, int finish_index2, int num_threads) {
    int arrSum[arr_size];
    double t1 = omp_get_wtime();
    long long sum;
    int sumMin;
    int rowMinIndex;
#pragma omp parallel for num_threads(num_threads)

    for (int i = start_index; i < finish_index; i++) {
        sum = 0;
        sumMin = arrSum[0];
        rowMinIndex = 0;
        for (int j = start_index2; j < finish_index2; j++) {
            sum += arr[i][j];
        }
        arrSum[i] = sum;
        if (arrSum[i] < sumMin)
        {
#pragma omp critical
            if (arrSum[i] < sumMin)
            {
                sumMin = arrSum[i];
                rowMinIndex = i;
            }
        }
    }

    double t2 = omp_get_wtime();

    cout << "The minimum sum is in a row " << rowMinIndex + 1 << ", Thread - " << num_threads << ", time -  " << t2 - t1 << " seconds" << endl;

    return sumMin;
}
